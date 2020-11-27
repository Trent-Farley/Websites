
$(document).ready(() => {

    $.getJSON('/Home/GetTags', (data) => {
        console.table(data);
        data.forEach((label) => {
            $('#checks').append(`
            
            `);
        });
    })

});

