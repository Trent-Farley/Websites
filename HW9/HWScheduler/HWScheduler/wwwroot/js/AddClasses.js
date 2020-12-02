$("#classAdd").on('submit', () => {
    const courses = $("#classes").val()
        .toUpperCase()
        .replace(/\s/g, "")
        .split(",");
    $('#classes').val(courses);

});
