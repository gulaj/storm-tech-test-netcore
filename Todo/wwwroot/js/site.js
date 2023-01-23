// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


 $('#hide-done').change(function () {
     $taskDone = $('.done').parents("li");
     if (!this.checked) {
      
         $taskDone.show();
    }
    else {
         $taskDone.hide();

    }


});