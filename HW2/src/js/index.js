const root = $(".container");
let append = (el, content, classname,attribute) => {
    if (classname === undefined && attribute === undefined) {
        root.append(`<${el}> ${content} <${el}>`)
    } else if(attribute === undefined) {
        root.append(`<${el} class="${classname}"> ${content} <${el}>`)
    } else{
        root.append(`<${el} class="${classname}" ${attribute}> ${content} <${el}>`)
    }
};



