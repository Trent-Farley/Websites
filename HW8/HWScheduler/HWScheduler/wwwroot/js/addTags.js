$(document).ready(() => {
    getTags()
})
let getTags = () => {
    $.getJSON('/api/GetTags', (data) => { showTags(data) });
}

let showTags = (tags) => {
    $('#checks').empty();
    tags.forEach((tag) => {

        $('#checks').append(`
            <div class="form-check form-check-inline">
                <input name="Tags" class="form-check-input" type="checkbox" id="tag_${tag.tagname}" value="${tag.id}">
                <label class="form-check-label" for="tag_${tag.tagname}">${tag.tagname}</label>
           </div>
        `)
    })

}

let sendTags = async () => {
    tagname = $('#newT').val();
    await $.post('/api/AddTag?tagname=' + tagname);
    getTags();
}
