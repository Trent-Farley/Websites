$(document).ready(function () {
    $(".calculator").hide(); 
});

$(".form-group").submit(function (e) { 
    e.preventDefault();
    let color = $("#color").val().toLowerCase();
    let colors = ["red", "green", "blue", "pink", "yellow", "orange", "cyan","gray"]
    if (!colors.includes(color)) {
        $(".forms").append(`<h7 class="card-title btn-danger">Sorry only these colors are allowed:<br> ${colors.join("<br>")}<h7>`);
    } else {
        $(".calculator").css("background-color", `${color}`);
        $(".forms").hide();
        $(".calculator").show();
    }
});

setInterval(() => {
    let bar = $(".bar");
    bar.fadeTo(100, 1.0, function () {
        $(this).fadeTo(500, 0.0);
    });
}, 1500);

let calculations = {
    ans: NaN,
    symbol: NaN,
};

let symbolClick = (symbol) => {
    if (typeof calculations.symbol === "string") {
        $('.numScreen').text("Too many operations!");
    } else {
        $('.numScreen').append(`${symbol}`)
        calculations.symbol = symbol;
    }
}
let showNum = (item) => {
    if (calculations.ans) {
        clears();
    }
    $('.numScreen').append(`${item}`);
}
let clears = () => {
    calculations.ans = NaN;
    calculations.symbol = NaN;
    $('.numScreen').text("  ");
}
let calculateScreen = () => {
    nums = $('.numScreen').text().split(calculations.symbol);
    let res = calculate(nums, calculations.symbol);
    $('.numScreen').text(`${res}`)
}
let calculate = (nums, symbol) => {
    let num1 = parseInt(nums[0]);
    let num2 = parseInt(nums[1]);
    let ans = 0;
    switch (symbol) {
        case "*":
            ans = num1 * num2;
            break;
        case "-":
            ans = num1 - num2;
            break;
        case "+":
            ans = num1 + num2;
            break;
        case "/":
            ans = num1 / num2;
            break;
    }
    if (!ans) {
        calculations.ans = 7;
        return 0;
    }
    calculations.ans = ans;
    return ans;
};
