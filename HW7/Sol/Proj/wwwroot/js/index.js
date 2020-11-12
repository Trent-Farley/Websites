$(document).ready(() => {
    getUser();
    getRepos();
})

let getUser = async () => {
    await $.getJSON("/api/User", (data) => {
        $("#userImg").attr("src", data.img);
        $("#userName").text(data.name);
        $("#userLocation").text(data.location);
        $("#userEmail").text(data.email);
        $("#userCompany").text(data.company);
    }).fail(() => { console.log("failed to get json in user") });
    $("#content").css("display", "block");
}

let getRepos = async () => {
    await $.getJSON("/api/Repos", (data) => {
        data.forEach(repo => addRepo(repo))
    });
}

let addRepo = (repo) => {
    let repoHTML = `
    <div class="card">
        <div class="card-body">
        <div class="row">
            <div class="col">
                <img  src="${repo.ownerAvatarUrl}" alt="Profile photo" class="img-thumbnail" />
            </div>
            <div class="col">
                <h6 class="card-title">${repo.name}</h6>
                <p class="card-text">
                    Owner login: ${repo.ownerLogin}<br>
                    Last Push: ${repo.pushedAt}
                </p>
            </div>
        </div>
        </div>
    </div>
    `
    $("#repos").append(repoHTML);
}