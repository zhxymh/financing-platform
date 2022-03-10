$(function () {
    var l = abp.localization.getResource("Financing");
	
	var enterpriseDetailService = window.tank.financing.enterpriseDetails.enterpriseDetails;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "EnterpriseDetails/CreateModal",
        scriptUrl: "/Pages/EnterpriseDetails/createModal.js",
        modalClass: "enterpriseDetailCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "EnterpriseDetails/EditModal",
        scriptUrl: "/Pages/EnterpriseDetails/editModal.js",
        modalClass: "enterpriseDetailEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            enterpriseName: $("#EnterpriseNameFilter").val(),
			totalAssets: $("#TotalAssetsFilter").val(),
			income: $("#IncomeFilter").val(),
			enterpriseType: $("#EnterpriseTypeFilter").val(),
			staffNumberMin: $("#StaffNumberFilterMin").val(),
			staffNumberMax: $("#StaffNumberFilterMax").val(),
			industry: $("#IndustryFilter").val(),
			location: $("#LocationFilter").val(),
			registeredAddress: $("#RegisteredAddressFilter").val(),
			businessAddress: $("#BusinessAddressFilter").val(),
			businessScope: $("#BusinessScopeFilter").val(),
			description: $("#DescriptionFilter").val(),
			completeTxId: $("#CompleteTxIdFilter").val()
        };
    };

    var dataTable = $("#EnterpriseDetailsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(enterpriseDetailService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('Financing.EnterpriseDetails.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('Financing.EnterpriseDetails.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    enterpriseDetailService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "enterpriseName" },
			{ data: "totalAssets" },
			{ data: "income" },
			{ data: "enterpriseType" },
			{ data: "staffNumber" },
			{ data: "industry" },
			{ data: "location" },
			{ data: "registeredAddress" },
			{ data: "businessAddress" },
			{ data: "businessScope" },
			{ data: "description" },
			{ data: "completeTxId" }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewEnterpriseDetailButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reload();
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reload();
    });
});
