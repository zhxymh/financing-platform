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
using Tank.Financing.Applies;
using Tank.Financing.Permissions;
using Tank.Financing.Shared;

namespace Tank.Financing.Blazor.Pages
{
    public partial class Applies
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        private IReadOnlyList<ApplyDto> ApplyList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }
        private bool CanCreateApply { get; set; }
        private bool CanEditApply { get; set; }
        private bool CanDeleteApply { get; set; }
        private ApplyCreateDto NewApply { get; set; }
        private Validations NewApplyValidations { get; set; }
        private ApplyUpdateDto EditingApply { get; set; }
        private Validations EditingApplyValidations { get; set; }
        private Guid EditingApplyId { get; set; }
        private Modal CreateApplyModal { get; set; }
        private Modal EditApplyModal { get; set; }
        private GetAppliesInput Filter { get; set; }
        private DataGridEntityActionsColumn<ApplyDto> EntityActionsColumn { get; set; }
        
        public Applies()
        {
            NewApply = new ApplyCreateDto();
            EditingApply = new ApplyUpdateDto();
            Filter = new GetAppliesInput
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
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:Applies"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["NewApply"], async () =>
            {
                await OpenCreateApplyModalAsync();
            }, IconName.Add, requiredPolicyName: FinancingPermissions.Applies.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateApply = await AuthorizationService
                .IsGrantedAsync(FinancingPermissions.Applies.Create);
            CanEditApply = await AuthorizationService
                            .IsGrantedAsync(FinancingPermissions.Applies.Edit);
            CanDeleteApply = await AuthorizationService
                            .IsGrantedAsync(FinancingPermissions.Applies.Delete);
        }

        private async Task GetAppliesAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await AppliesAppService.GetListAsync(Filter);
            ApplyList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetAppliesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<ApplyDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.Direction != SortDirection.None)
                .Select(c => c.Field + (c.Direction == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetAppliesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateApplyModalAsync()
        {
            NewApply = new ApplyCreateDto{
                
                
            };
            await NewApplyValidations.ClearAll();
            await CreateApplyModal.Show();
        }

        private async Task CloseCreateApplyModalAsync()
        {
            await CreateApplyModal.Hide();
        }

        private async Task OpenEditApplyModalAsync(ApplyDto input)
        {
            EditingApplyId = input.Id;
            EditingApply = ObjectMapper.Map<ApplyDto, ApplyUpdateDto>(input);
            await EditingApplyValidations.ClearAll();
            await EditApplyModal.Show();
        }

        private async Task DeleteApplyAsync(ApplyDto input)
        {
            await AppliesAppService.DeleteAsync(input.Id);
            await GetAppliesAsync();
        }

        private async Task CreateApplyAsync()
        {
            try
            {
                if (await NewApplyValidations.ValidateAll() == false)
                {
                    return;
                }

                await AppliesAppService.CreateAsync(NewApply);
                await GetAppliesAsync();
                await CreateApplyModal.Hide();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditApplyModalAsync()
        {
            await EditApplyModal.Hide();
        }

        private async Task UpdateApplyAsync()
        {
            try
            {
                if (await EditingApplyValidations.ValidateAll() == false)
                {
                    return;
                }

                await AppliesAppService.UpdateAsync(EditingApplyId, EditingApply);
                await GetAppliesAsync();
                await EditApplyModal.Hide();                
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

    }
}
