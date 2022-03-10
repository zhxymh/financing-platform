$(function () {
    var l = abp.localization.getResource("Financing");
	
	var enterpriseService = window.tank.financing.enterprises.enterprises;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Enterprises/CreateModal",
        scriptUrl: "/Pages/Enterprises/createModal.js",
        modalClass: "enterpriseCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Enterprises/EditModal",
        scriptUrl: "/Pages/Enterprises/editModal.js",
        modalClass: "enterpriseEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            enterpriseName: $("#EnterpriseNameFilter").val(),
			artificialPerson: $("#ArtificialPersonFilter").val(),
			establishedTimeMin: $("#EstablishedTimeFilterMin").val(),
			establishedTimeMax: $("#EstablishedTimeFilterMax").val(),
			dueTimeMin: $("#DueTimeFilterMin").val(),
			dueTimeMax: $("#DueTimeFilterMax").val(),
			creditCode: $("#CreditCodeFilter").val(),
			artificialPersonId: $("#ArtificialPersonIdFilter").val(),
			registeredCapital: $("#RegisteredCapitalFilter").val(),
			phoneNumber: $("#PhoneNumberFilter").val(),
			certPhotoPath: $("#CertPhotoPathFilter").val(),
			idPhotoPath1: $("#IdPhotoPath1Filter").val(),
			idPhotoPath2: $("#IdPhotoPath2Filter").val(),
			certificateStatus: $("#CertificateStatusFilter").val(),
			certificateTxId: $("#CertificateTxIdFilter").val(),
			confirmCertificateTxId: $("#ConfirmCertificateTxIdFilter").val()
        };
    };

    var dataTable = $("#EnterprisesTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(enterpriseService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('Financing.Enterprises.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('Financing.Enterprises.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    enterpriseService.delete(data.record.id)
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
			{ data: "artificialPerson" },
			{ data: "establishedTime" },
			{ data: "dueTime" },
			{ data: "creditCode" },
			{ data: "artificialPersonId" },
			{ data: "registeredCapital" },
			{ data: "phoneNumber" },
			{ data: "certPhotoPath" },
			{ data: "idPhotoPath1" },
			{ data: "idPhotoPath2" },
            {
                data: "certificateStatus",
                render: function (certificateStatus) {
                    if (certificateStatus === undefined ||
                        certificateStatus === null) {
                        return "";
                    }

                    var localizationKey = "Enum:CertificateStatus:" + certificateStatus;
                    var localized = l(localizationKey);

                    if (localized === localizationKey) {
                        abp.log.warn("No localization found for " + localizationKey);
                        return "";
                    }

                    return localized;
                }
            },
			{ data: "certificateTxId" },
			{ data: "confirmCertificateTxId" }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewEnterpriseButton").click(function (e) {
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
