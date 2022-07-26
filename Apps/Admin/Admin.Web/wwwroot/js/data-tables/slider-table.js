"use strict";
var KTDatatable = function () {
	var init = async (data) => {
		$("#kt_datatable_example_5").DataTable({
			searchDelay: 500,
			processing: true,
			data,
			stateSave: true,
			columns: [
				{ data: 'title' },
				{ data: 'imagePath' },
				{
					data: 'id',
					render: (data) => {
						return `
							<a href="/slider/update/${data}" class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1">
								<span class="svg-icon svg-icon-3">
									<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
										<path opacity="0.3" d="M21.4 8.35303L19.241 10.511L13.485 4.755L15.643 2.59595C16.0248 2.21423 16.5426 1.99988 17.0825 1.99988C17.6224 1.99988 18.1402 2.21423 18.522 2.59595L21.4 5.474C21.7817 5.85581 21.9962 6.37355 21.9962 6.91345C21.9962 7.45335 21.7817 7.97122 21.4 8.35303ZM3.68699 21.932L9.88699 19.865L4.13099 14.109L2.06399 20.309C1.98815 20.5354 1.97703 20.7787 2.03189 21.0111C2.08674 21.2436 2.2054 21.4561 2.37449 21.6248C2.54359 21.7934 2.75641 21.9115 2.989 21.9658C3.22158 22.0201 3.4647 22.0084 3.69099 21.932H3.68699Z" fill="black"></path>
										<path d="M5.574 21.3L3.692 21.928C3.46591 22.0032 3.22334 22.0141 2.99144 21.9594C2.75954 21.9046 2.54744 21.7864 2.3789 21.6179C2.21036 21.4495 2.09202 21.2375 2.03711 21.0056C1.9822 20.7737 1.99289 20.5312 2.06799 20.3051L2.696 18.422L5.574 21.3ZM4.13499 14.105L9.891 19.861L19.245 10.507L13.489 4.75098L4.13499 14.105Z" fill="black"></path>
									</svg>
								</span>
							</a>
							<a href="#" onclick="Delete('${data}')" class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1">
								<span class="svg-icon svg-icon-3">
									<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
										<path d="M5 9C5 8.44772 5.44772 8 6 8H18C18.5523 8 19 8.44772 19 9V18C19 19.6569 17.6569 21 16 21H8C6.34315 21 5 19.6569 5 18V9Z" fill="black"></path>
										<path opacity="0.5" d="M5 5C5 4.44772 5.44772 4 6 4H18C18.5523 4 19 4.44772 19 5V5C19 5.55228 18.5523 6 18 6H6C5.44772 6 5 5.55228 5 5V5Z" fill="black"></path>
										<path opacity="0.5" d="M9 4C9 3.44772 9.44772 3 10 3H14C14.5523 3 15 3.44772 15 4V4H9V4Z" fill="black"></path>
									</svg>
								</span>
							</a>
							`;
					}
				}
			],
			language: {
				"lengthMenu": "Show _MENU_",
			},
			dom:
				"<'row'" +
				"<'col-sm-6 d-flex align-items-center justify-conten-start'l>" +
				"<'col-sm-6 d-flex align-items-center justify-content-end'f>" +
				">" +

				"<'table-responsive'tr>" +

				"<'row'" +
				"<'col-sm-12 col-md-5 d-flex align-items-center justify-content-center justify-content-md-start'i>" +
				"<'col-sm-12 col-md-7 d-flex align-items-center justify-content-center justify-content-md-end'p>" +
				">"
		});
	}
	var reload = function (data) {
		$('#kt_datatable_example_5').DataTable().clear().rows.add(data).draw()
	};
	var trigger = function (btn) {
		$('#kt_datatable_example_5').DataTable().button(btn).trigger();
	};
	return {
		init: init,
		reload: reload,
		trigger: trigger,
	}
}();
var Delete = async (id) => {
	Swal.fire({
		text: "Are you sure you want to delete selected customers?",
		icon: "warning",
		showCancelButton: true,
		buttonsStyling: false,
		showLoaderOnConfirm: true,
		confirmButtonText: "Yes, delete!",
		cancelButtonText: "No, cancel",
		customClass: {
			confirmButton: "btn fw-bold btn-danger",
			cancelButton: "btn fw-bold btn-active-light-primary"
		},
	}).then(async function (result) {
		if (result.value) {
			await $.ajax({
				url: `/slider/delete/${id}`,
				method: "DELETE",
				success: async function () {
					Swal.fire({
						text: "Record Deleted Successfully",
						icon: "success",
						buttonsStyling: false,
						confirmButtonText: "Ok, got it!",
						customClass: {
							confirmButton: "btn fw-bold btn-primary",
						}
					});
					KTDatatable.reload(await getData("/slider/getdata"));
				}
			});
		}
	});
}
KTUtil.onDOMContentLoaded(async function () {
	KTDatatable.init(await getData("/slider/getdata"));
});