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
using Tank.Financing.EnterpriseDetails;
using Tank.Financing.Permissions;
using Tank.Financing.Shared;

namespace Tank.Financing.Blazor.Pages
{
    public partial class EnterpriseDetails
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        private IReadOnlyList<EnterpriseDetailDto> EnterpriseDetailList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }
        private bool CanCreateEnterpriseDetail { get; set; }
        private bool CanEditEnterpriseDetail { get; set; }
        private bool CanDeleteEnterpriseDetail { get; set; }
        private EnterpriseDetailCreateDto NewEnterpriseDetail { get; set; }
        private Validations NewEnterpriseDetailValidations { get; set; }
        private EnterpriseDetailUpdateDto EditingEnterpriseDetail { get; set; }
        private Validations EditingEnterpriseDetailValidations { get; set; }
        private Guid EditingEnterpriseDetailId { get; set; }
        private Modal CreateEnterpriseDetailModal { get; set; }
        private Modal EditEnterpriseDetailModal { get; set; }
        private GetEnterpriseDetailsInput Filter { get; set; }
        private DataGridEntityActionsColumn<EnterpriseDetailDto> EntityActionsColumn { get; set; }
        
        public EnterpriseDetails()
        {
            NewEnterpriseDetail = new EnterpriseDetailCreateDto();
            EditingEnterpriseDetail = new EnterpriseDetailUpdateDto();
            Filter = new GetEnterpriseDetailsInput
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
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:EnterpriseDetails"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["NewEnterpriseDetail"], async () =>
            {
                await OpenCreateEnterpriseDetailModalAsync();
            }, IconName.Add, requiredPolicyName: FinancingPermissions.EnterpriseDetails.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateEnterpriseDetail = await AuthorizationService
                .IsGrantedAsync(FinancingPermissions.EnterpriseDetails.Create);
            CanEditEnterpriseDetail = await AuthorizationService
                            .IsGrantedAsync(FinancingPermissions.EnterpriseDetails.Edit);
            CanDeleteEnterpriseDetail = await AuthorizationService
                            .IsGrantedAsync(FinancingPermissions.EnterpriseDetails.Delete);
        }

        private async Task GetEnterpriseDetailsAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await EnterpriseDetailsAppService.GetListAsync(Filter);
            EnterpriseDetailList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetEnterpriseDetailsAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<EnterpriseDetailDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetEnterpriseDetailsAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateEnterpriseDetailModalAsync()
        {
            NewEnterpriseDetail = new EnterpriseDetailCreateDto{
                
                
            };
            await NewEnterpriseDetailValidations.ClearAll();
            await CreateEnterpriseDetailModal.Show();
        }

        private async Task CloseCreateEnterpriseDetailModalAsync()
        {
            await CreateEnterpriseDetailModal.Hide();
        }

        private async Task OpenEditEnterpriseDetailModalAsync(EnterpriseDetailDto input)
        {
            EditingEnterpriseDetailId = input.Id;
            EditingEnterpriseDetail = ObjectMapper.Map<EnterpriseDetailDto, EnterpriseDetailUpdateDto>(input);
            await EditingEnterpriseDetailValidations.ClearAll();
            await EditEnterpriseDetailModal.Show();
        }

        private async Task DeleteEnterpriseDetailAsync(EnterpriseDetailDto input)
        {
            await EnterpriseDetailsAppService.DeleteAsync(input.Id);
            await GetEnterpriseDetailsAsync();
        }

        private async Task CreateEnterpriseDetailAsync()
        {
            try
            {
                if (await NewEnterpriseDetailValidations.ValidateAll() == false)
                {
                    return;
                }

                await EnterpriseDetailsAppService.CreateAsync(NewEnterpriseDetail);
                await GetEnterpriseDetailsAsync();
                await CreateEnterpriseDetailModal.Hide();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditEnterpriseDetailModalAsync()
        {
            await EditEnterpriseDetailModal.Hide();
        }

        private async Task UpdateEnterpriseDetailAsync()
        {
            try
            {
                if (await EditingEnterpriseDetailValidations.ValidateAll() == false)
                {
                    return;
                }

                await EnterpriseDetailsAppService.UpdateAsync(EditingEnterpriseDetailId, EditingEnterpriseDetail);
                await GetEnterpriseDetailsAsync();
                await EditEnterpriseDetailModal.Hide();                
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

    }
}
