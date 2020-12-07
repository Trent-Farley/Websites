// Write your JavaScript code.

// This will set up a timer.  It will invoke the execute function every 5 seconds
$(document).ready(function () { window.setInterval(execute, 5000) });
let count = 0;
function execute() {
    console.log('Running execute function');
    if (count % 2) {
        console.clear();
        console.warn('Running execute');
    }
}