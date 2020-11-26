
let getClasses = () => {

    const courses = $("#Classes").val()
        .toUpperCase()
        .replace(/\s/g, "")
        .split(",");

    $.ajax({
        type: 'POST',
        url: '/Home/AddClasses',
        data: { classes: courses },
        dataType: "json",
        success: () => {

        },
        error: (e) => {
            console.log(e);
        }
    })
    window.location.href = "/Home";
    return false;
}