$(function () {
    var l = abp.localization.getResource("Financing");
	
	var applyService = window.tank.financing.applies.applies;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Applies/CreateModal",
        scriptUrl: "/Pages/Applies/createModal.js",
        modalClass: "applyCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Applies/EditModal",
        scriptUrl: "/Pages/Applies/editModal.js",
        modalClass: "applyEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            enterpriseName: $("#EnterpriseNameFilter").val(),
			organization: $("#OrganizationFilter").val(),
			productName: $("#ProductNameFilter").val(),
			allowance: $("#AllowanceFilter").val(),
			aPR: $("#APRFilter").val(),
			period: $("#PeriodFilter").val(),
			applyStatus: $("#ApplyStatusFilter").val(),
			guaranteeMethod: $("#GuaranteeMethodFilter").val(),
			applyTimeMin: $("#ApplyTimeFilterMin").val(),
			applyTimeMax: $("#ApplyTimeFilterMax").val(),
			passedTimeMin: $("#PassedTimeFilterMin").val(),
			passedTimeMax: $("#PassedTimeFilterMax").val()
        };
    };

    var dataTable = $("#AppliesTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(applyService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('Financing.Applies.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('Financing.Applies.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    applyService.delete(data.record.id)
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
			{ data: "organization" },
			{ data: "productName" },
			{ data: "allowance" },
			{ data: "apr" },
			{ data: "period" },
            {
                data: "applyStatus",
                render: function (applyStatus) {
                    if (applyStatus === undefined ||
                        applyStatus === null) {
                        return "";
                    }

                    var localizationKey = "Enum:ApplyStatus:" + applyStatus;
                    var localized = l(localizationKey);

                    if (localized === localizationKey) {
                        abp.log.warn("No localization found for " + localizationKey);
                        return "";
                    }

                    return localized;
                }
            },
            {
                data: "guaranteeMethod",
                render: function (guaranteeMethod) {
                    if (guaranteeMethod === undefined ||
                        guaranteeMethod === null) {
                        return "";
                    }

                    var localizationKey = "Enum:GuaranteeMethod:" + guaranteeMethod;
                    var localized = l(localizationKey);

                    if (localized === localizationKey) {
                        abp.log.warn("No localization found for " + localizationKey);
                        return "";
                    }

                    return localized;
                }
            },
			{ data: "applyTime" },
			{ data: "passedTime" }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewApplyButton").click(function (e) {
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
