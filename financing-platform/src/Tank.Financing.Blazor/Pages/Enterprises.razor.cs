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
using Tank.Financing.Enterprises;
using Tank.Financing.Permissions;
using Tank.Financing.Shared;

namespace Tank.Financing.Blazor.Pages
{
    public partial class Enterprises
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        private IReadOnlyList<EnterpriseDto> EnterpriseList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }
        private bool CanCreateEnterprise { get; set; }
        private bool CanEditEnterprise { get; set; }
        private bool CanDeleteEnterprise { get; set; }
        private EnterpriseCreateDto NewEnterprise { get; set; }
        private Validations NewEnterpriseValidations { get; set; }
        private EnterpriseUpdateDto EditingEnterprise { get; set; }
        private Validations EditingEnterpriseValidations { get; set; }
        private Guid EditingEnterpriseId { get; set; }
        private Modal CreateEnterpriseModal { get; set; }
        private Modal EditEnterpriseModal { get; set; }
        private GetEnterprisesInput Filter { get; set; }
        private DataGridEntityActionsColumn<EnterpriseDto> EntityActionsColumn { get; set; }
        
        public Enterprises()
        {
            NewEnterprise = new EnterpriseCreateDto();
            EditingEnterprise = new EnterpriseUpdateDto();
            Filter = new GetEnterprisesInput
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
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:Enterprises"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["NewEnterprise"], async () =>
            {
                await OpenCreateEnterpriseModalAsync();
            }, IconName.Add, requiredPolicyName: FinancingPermissions.Enterprises.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateEnterprise = await AuthorizationService
                .IsGrantedAsync(FinancingPermissions.Enterprises.Create);
            CanEditEnterprise = await AuthorizationService
                            .IsGrantedAsync(FinancingPermissions.Enterprises.Edit);
            CanDeleteEnterprise = await AuthorizationService
                            .IsGrantedAsync(FinancingPermissions.Enterprises.Delete);
        }

        private async Task GetEnterprisesAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await EnterprisesAppService.GetListAsync(Filter);
            EnterpriseList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetEnterprisesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<EnterpriseDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetEnterprisesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateEnterpriseModalAsync()
        {
            NewEnterprise = new EnterpriseCreateDto{
                
                
            };
            await NewEnterpriseValidations.ClearAll();
            await CreateEnterpriseModal.Show();
        }

        private async Task CloseCreateEnterpriseModalAsync()
        {
            await CreateEnterpriseModal.Hide();
        }

        private async Task OpenEditEnterpriseModalAsync(EnterpriseDto input)
        {
            EditingEnterpriseId = input.Id;
            EditingEnterprise = ObjectMapper.Map<EnterpriseDto, EnterpriseUpdateDto>(input);
            await EditingEnterpriseValidations.ClearAll();
            await EditEnterpriseModal.Show();
        }

        private async Task DeleteEnterpriseAsync(EnterpriseDto input)
        {
            await EnterprisesAppService.DeleteAsync(input.Id);
            await GetEnterprisesAsync();
        }

        private async Task CreateEnterpriseAsync()
        {
            try
            {
                if (await NewEnterpriseValidations.ValidateAll() == false)
                {
                    return;
                }

                await EnterprisesAppService.CreateAsync(NewEnterprise);
                await GetEnterprisesAsync();
                await CreateEnterpriseModal.Hide();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditEnterpriseModalAsync()
        {
            await EditEnterpriseModal.Hide();
        }

        private async Task UpdateEnterpriseAsync()
        {
            try
            {
                if (await EditingEnterpriseValidations.ValidateAll() == false)
                {
                    return;
                }

                await EnterprisesAppService.UpdateAsync(EditingEnterpriseId, EditingEnterprise);
                await GetEnterprisesAsync();
                await EditEnterpriseModal.Hide();                
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

    }
}
