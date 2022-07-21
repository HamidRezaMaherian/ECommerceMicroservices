"use strict";
const initDropZone = (selector, targetSelector, maxFiles = 1) => {
	var myDropzone = new Dropzone(selector, {
		url: "https://keenthemes.com/scripts/void.php", // Set the url for your upload script location
		paramName: "file", // The name that will be used to transfer the file
		autoProcessQueue: false,
		maxFiles: maxFiles,
		maxFilesize: 5, // MB
		addRemoveLinks: true,
	});
	myDropzone.on("addedfile", async (file) => {
		var base64 = await convertToBase64(file);
		document.querySelector(targetSelector).value += base64;
	});
	myDropzone.on("removedfile", async (file) => {
		var base64 = await convertToBase64(file);
		document.querySelector(targetSelector).value -= base64;
	});
};
const convertToBase64 = async (blob) => {
	return new Promise((resolve, _) => {
		const reader = new FileReader();
		reader.onloadend = () => resolve(reader.result);
		reader.readAsDataURL(blob);
	});
};
const getData = async function (endpoint, parse = false) {
	var data;
	await $.ajax({
		url: endpoint,
		success: function (result) {
			data = result;
		}
	})
	return parse === false ? data : JSON.parse(data);
}
