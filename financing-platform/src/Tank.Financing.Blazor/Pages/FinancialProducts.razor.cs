using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using Volo.Abp.BlazoriseUI.Components;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Tank.Financing.FinancialProducts;
using Tank.Financing.Permissions;
using Tank.Financing.Shared;

namespace Tank.Financing.Blazor.Pages
{
    public partial class FinancialProducts
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        private IReadOnlyList<FinancialProductDto> FinancialProductList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }
        private bool CanCreateFinancialProduct { get; set; }
        private bool CanEditFinancialProduct { get; set; }
        private bool CanDeleteFinancialProduct { get; set; }
        private FinancialProductCreateDto NewFinancialProduct { get; set; }
        private Validations NewFinancialProductValidations { get; set; }
        private FinancialProductUpdateDto EditingFinancialProduct { get; set; }
        private Validations EditingFinancialProductValidations { get; set; }
        private Guid EditingFinancialProductId { get; set; }
        private Modal CreateFinancialProductModal { get; set; }
        private Modal EditFinancialProductModal { get; set; }
        private GetFinancialProductsInput Filter { get; set; }
        private DataGridEntityActionsColumn<FinancialProductDto> EntityActionsColumn { get; set; }
        
        public FinancialProducts()
        {
            NewFinancialProduct = new FinancialProductCreateDto();
            EditingFinancialProduct = new FinancialProductUpdateDto();
            Filter = new GetFinancialProductsInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:FinancialProducts"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["NewFinancialProduct"], async () =>
            {
                await OpenCreateFinancialProductModalAsync();
            }, IconName.Add, requiredPolicyName: FinancingPermissions.FinancialProducts.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateFinancialProduct = await AuthorizationService
                .IsGrantedAsync(FinancingPermissions.FinancialProducts.Create);
            CanEditFinancialProduct = await AuthorizationService
                            .IsGrantedAsync(FinancingPermissions.FinancialProducts.Edit);
            CanDeleteFinancialProduct = await AuthorizationService
                            .IsGrantedAsync(FinancingPermissions.FinancialProducts.Delete);
        }

        private async Task GetFinancialProductsAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await FinancialProductsAppService.GetListAsync(Filter);
            FinancialProductList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetFinancialProductsAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<FinancialProductDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.Direction != SortDirection.None)
                .Select(c => c.Field + (c.Direction == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetFinancialProductsAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateFinancialProductModalAsync()
        {
            NewFinancialProduct = new FinancialProductCreateDto{
                
                
            };
            await NewFinancialProductValidations.ClearAll();
            await CreateFinancialProductModal.Show();
        }

        private async Task CloseCreateFinancialProductModalAsync()
        {
            await CreateFinancialProductModal.Hide();
        }

        private async Task OpenEditFinancialProductModalAsync(FinancialProductDto input)
        {
            EditingFinancialProductId = input.Id;
            EditingFinancialProduct = ObjectMapper.Map<FinancialProductDto, FinancialProductUpdateDto>(input);
            await EditingFinancialProductValidations.ClearAll();
            await EditFinancialProductModal.Show();
        }

        private async Task DeleteFinancialProductAsync(FinancialProductDto input)
        {
            await FinancialProductsAppService.DeleteAsync(input.Id);
            await GetFinancialProductsAsync();
        }

        private async Task CreateFinancialProductAsync()
        {
            try
            {
                if (await NewFinancialProductValidations.ValidateAll() == false)
                {
                    return;
                }

                await FinancialProductsAppService.CreateAsync(NewFinancialProduct);
                await GetFinancialProductsAsync();
                await CreateFinancialProductModal.Hide();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditFinancialProductModalAsync()
        {
            await EditFinancialProductModal.Hide();
        }

        private async Task UpdateFinancialProductAsync()
        {
            try
            {
                if (await EditingFinancialProductValidations.ValidateAll() == false)
                {
                    return;
                }

                await FinancialProductsAppService.UpdateAsync(EditingFinancialProductId, EditingFinancialProduct);
                await GetFinancialProductsAsync();
                await EditFinancialProductModal.Hide();                
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

    }
}
