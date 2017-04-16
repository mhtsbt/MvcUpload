# MvcUpload

Simple put the following-line inside your "Configure"-method:

    public void Configure(IApplicationBuilder app) {
    
      app.UseMvcUpload(new MvcUploadOptions() {
        Route = "/upload",
        UploadsFolder = "Content"
      });
    }
    
It will detect any POST-method containing files, and copy them to the selected folder

## CkEditor configuration

    CKEDITOR.replace('editor', {
            extraPlugins: 'uploadimage',
            imageUploadUrl: '/upload',
            uploadUrl: '/upload',
        });
