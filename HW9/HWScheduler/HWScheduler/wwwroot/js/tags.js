

$("form").on("submit", function (event) {
    event.preventDefault();
    postArr = JSON.stringify(correctArray($('form').serializeArray()));

    $.ajax({
        url: '/Home/Create',
        data: { hw: postArr.toString() },
        type: 'POST',
        success: (data) => {
            if (data.success != true) {
                console.log(errors);
            } else {
                window.location.replace("/Home")
            }
        },
        error: (data) => {
            console.log(data);
        }
    })

});

let correctArray = (arr) => {
    let hw = { "Tags": [] };
    for (let i = 0; i < arr.length; ++i) {
        if (hw[arr[i].name]) {
            hw[arr[i].name].push(arr[i].value)
        } else {
            hw[arr[i].name] = arr[i].value;
        }
    }
    return hw;
}