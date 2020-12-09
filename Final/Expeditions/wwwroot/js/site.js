// Write your JavaScript code.

// This will set up a timer.  It will invoke the execute function every 5 seconds
$(document).ready(function () { window.setInterval(execute, 5000) });
let count = 0;
function execute() {
    $.getJSON('/api/GetStats', (data) => {
        showStats(data);
    });

}
//otalExps = exps, totalPeaks = peaks, totalUnclimbed = unclimbed
let showStats = (data) => {
    $('#stats').empty();
    $('#stats').fadeOut();
    setTimeout(2000);
    $('#stats').append(`Currently tracking ${data.totalExps} expeditions for ${data.totalPeaks} peaks, ${data.totalUnclimbed} of which have not been climbed!`);

    $('#stats').fadeIn();

}