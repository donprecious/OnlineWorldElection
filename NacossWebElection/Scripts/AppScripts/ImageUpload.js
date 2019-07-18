﻿	var $uploadCrop,
						tempFilename,
						rawImg,
						imageId;
						function readFile(input) {
				 		if (input.files && input.files[0]) {
				              var reader = new FileReader();
					            reader.onload = function (e) {
									$('.upload-demo').addClass('ready');
									$('#cropImagePop').modal('show');
						            rawImg = e.target.result;
					            }
					            reader.readAsDataURL(input.files[0]);
					        }
					        else {
						        swal("Sorry - you're browser doesn't support the FileReader API");
						    }
						}
						$uploadCrop = $('#upload-demo').croppie({
							//viewport: {
							//	width: 300,
							//	height: 300,
							//	type: 'square',
						    //},
						    viewport: {
						        width: 300,
						        height: 300,
						        type: 'square',
						    },
						   
							enforceBoundary: false,
							enableExif: true
						});
						$('#cropImagePop').on('shown.bs.modal', function(){
							// alert('Shown pop');
							$uploadCrop.croppie('bind', {
				        		url: rawImg
				        	}).then(function(){
				        		console.log('jQuery bind complete');
				        	});
						});

						$('.item-img').on('change', function () { imageId = $(this).data('id'); tempFilename = $(this).val();
																										 $('#cancelCropBtn').data('id', imageId); readFile(this); });
						$('#cropImageBtn').on('click', function (ev) {
							$uploadCrop.croppie('result', {
							    //type: 'base64',
							     type: 'rawcanvas',
							   
								format: 'jpeg',
								//size: {width: 150, height: 200}
							}).then(function (resp) {
							    $('#item-img-output').attr('src', resp.toDataURL());
							    $('#cropImagePop').modal('hide');
							    console.log(resp);
							});
						});




				// End upload preview image