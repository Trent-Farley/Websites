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
    $("#repos").append(repoHTML(repo));
}

let getCommits = async (repoName) => {
    $("#commitBody").empty();
    $("#commitBody").append(table)
    await $.getJSON(`/api/Commits?repo=${repoName}`, (data) => {
        data.forEach(commit => {
            addCommits(commit);
        });
    }).fail(() => { console.log("Failed to get commits") });
    $("#commitBody").append("</table>")
    $("#commits").modal("toggle");
}

let table = `<table id="commitTable" class="table table-striped">
    <thead>
    <tr>
            <th>SHA</th>
            <th>Author</th>
            <th>Time</th>
            <th>Commit Mesage</th>
        </tr>
        <tbody>
        </tbody>
    </thead>
`

let addCommits = (commit) => {
    $("#commitTable > tbody:last-child")
        .append(`<tr>
        <td><a href="${commit.shaUrl}"> ${commit.sha.substr(-5)}</a></td>
        <td>${commit.authorName} </td>
        <td>${commit.timestamp} </td>
        <td>${commit.commitMessage}</td>
        </tr>`);
}

let repoHTML = (repo) => {
    let daysSince = Date.now();
    let daysNow = Date.parse(repo.pushedAt);
    let totalDays = daysBetween(daysNow, daysSince);
    return `
    <div class="card">
        <div class="card-body">
            <div class="row">
                <div class="col-sm-4">
                    <img src="${repo.ownerAvatarUrl}" alt="Profile photo" class="img-thumbnail" />
                </div>
                <div class="col">
                    <strong><h6 class="card-title">${repo.name}</h6></strong>
                    <p class="card-text">
                        <strong>Owner login</strong>: ${repo.ownerLogin}<br>
                        <strong>Last Push</strong>: ${totalDays} <i>days ago</i>
                    </p>
                    <button class="btr btn btn-outline-dark" onclick="getCommits('${repo.name}')" type="button"><h7><strong>&#8594;</strong></h7></button>
                </div>
            </div>

        </div>
    </div>
    `;
}

// I got these two functions here: https://stackoverflow.com/questions/542938/how-do-i-get-the-number-of-days-between-two-dates-in-javascript
// dates are the worst to deal withand I figured since that wasn't the point of the assignment I would look
// it up.
let treatAsUTC = (date) => {
    let result = new Date(date);
    result.setMinutes(result.getMinutes() - result.getTimezoneOffset());
    return result;
}

let daysBetween = (startDate, endDate) => {
    var millisecondsPerDay = 24 * 60 * 60 * 1000;
    return Math.round((treatAsUTC(endDate) - treatAsUTC(startDate)) / millisecondsPerDay);
}