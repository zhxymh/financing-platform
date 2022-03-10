$(function () {
    var l = abp.localization.getResource("Financing");
	
	var financialProductService = window.tank.financing.financialProducts.financialProducts;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "FinancialProducts/CreateModal",
        scriptUrl: "/Pages/FinancialProducts/createModal.js",
        modalClass: "financialProductCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "FinancialProducts/EditModal",
        scriptUrl: "/Pages/FinancialProducts/editModal.js",
        modalClass: "financialProductEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            productName: $("#ProductNameFilter").val(),
			organization: $("#OrganizationFilter").val(),
			periodMin: $("#PeriodFilterMin").val(),
			periodMax: $("#PeriodFilterMax").val(),
			guaranteeMethod: $("#GuaranteeMethodFilter").val(),
			appliedNumberMin: $("#AppliedNumberFilterMin").val(),
			appliedNumberMax: $("#AppliedNumberFilterMax").val(),
			aPR: $("#APRFilter").val(),
			rating: $("#RatingFilter").val(),
			creditCeilingMin: $("#CreditCeilingFilterMin").val(),
			creditCeilingMax: $("#CreditCeilingFilterMax").val()
        };
    };

    var dataTable = $("#FinancialProductsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(financialProductService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('Financing.FinancialProducts.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('Financing.FinancialProducts.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    financialProductService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "productName" },
			{ data: "organization" },
			{ data: "period" },
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
			{ data: "appliedNumber" },
			{ data: "apr" },
			{ data: "rating" },
			{ data: "creditCeiling" }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewFinancialProductButton").click(function (e) {
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
