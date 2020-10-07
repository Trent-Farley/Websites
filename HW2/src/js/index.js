

setInterval(() => {
    let bar = $(".bar");
    bar.fadeTo(100, 1.0, function() { $(this).fadeTo(500, 0.0); });
}, 1000);



let append = (el, content, classname,attribute) => {
    if (classname === undefined && attribute === undefined) {
        root.append(`<${el}> ${content} <${el}>`);
    } else if(attribute === undefined) {
        root.append(`<${el} class="${classname}"> ${content} <${el}>`);
    } else{
        root.append(`<${el} class="${classname}" ${attribute}> ${content} <${el}>`);
    }
};
let calculations = {
    nums:[],
    symbol:undefined
};

let showNum =(num) =>{
    if(typeof num === 'number'){
        calculations.nums.push(num);
        $('.numScreen').append(num);
    } else{
        switch(num){
            case 'enter':
                let calc = calculate(calculations.nums,calculations.symbol)
                calculations.nums = [];
                calculations.symbol = undefined;
                $(".numScreen").text(`${calc}`);
                break;
            case '*':
                calculations.symbol = "*"
                $(".numScreen").append("×");
                break;
            case '/':
                calculations.symbol = "/"
                $(".numScreen").append("/");
                break;
            case '-':
                calculations.symbol = "-"
                $(".numScreen").append("-");
                break;
            case '+':
                calculations.symbol = "+"
                $(".numScreen").append("+");
                break;
            case 'clear':
                $(".numScreen").text("");
                break;
            
        }
    }
    

}

let calculate = (nums, symbol)=>{
    console.log(symbol);
    console.log(nums);
    switch(symbol){
        case "*":
            return nums.reduce((a,b)=>a*b);
        case "-":
            return nums.reduce((a,b)=> b-a);
        case "+":
            return nums.reduce((a,b)=> a+b);
        case "/":
            return nums.reduce((a,b)=> b/a);
    }
}
