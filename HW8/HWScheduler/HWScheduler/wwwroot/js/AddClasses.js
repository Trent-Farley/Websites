$("#classAdd").on('submit', () => {
    console.log("hit");
    const courses = $("#classes").val()
        .toUpperCase()
        .replace(/\s/g, "")
        .split(",");

    $('#classes').val(courses);
    console.log($('#classes').val());
});
