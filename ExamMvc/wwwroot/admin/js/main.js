let deleteBtn = document.querySelectorAll(".deleteBtn");

deleteBtn.forEach(item => item.addEventListener("click", function (e) {

    e.preventDefault()

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            let url = item.getAttribute("href")
            fetch(url)
                .then(response => {

                    if (response.status==200) {
                        Swal.fire({
                            title: "Deleted!",
                            text: "Your file has been deleted.",
                            icon: "success"
                        });
                        item.parentElement.parentElement.remove()
                        window.location.reload(true)
                    }
                    else {
                        Swal.fire({
                            title: "Error!",
                            text: "Your file Could not deleted.",
                            icon: "error"
                        });
                    }
                })
          
        }
    });

}))