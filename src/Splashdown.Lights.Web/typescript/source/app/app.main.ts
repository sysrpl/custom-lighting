function main() {
    let frame = get("#frame");
    let front = get("#front");
    let back = get("#back");
    let shadow = get("#shadow");
    let edge = get("#edge");
    let fullscreen = get("#fullscreen");

    get("#done").addEventListener("click", () => {
        back.removeClass("flip").addClass("noflip");
        shadow.removeClass("flip").addClass("noflip");
        front.removeClass("flip").addClass("noflip");
        edge.removeClass("flip").addClass("noflip");
    });

    fullscreen.addEventListener("click", () => {
        frame.toggleClass("expand");
        if (frame.hasClass("expand")) {
            fullscreen.removeClass("fa-search-plus");
            fullscreen.addClass("fa-search-minus");
        } 
        else {
            fullscreen.removeClass("fa-search-minus");
            fullscreen.addClass("fa-search-plus");
        }
    });

    let now = new Date();
    let stamp = now.format("#DDD# #MM# #D# #h#:#mm#:#ss# #YYYY#");
    get("#stamp").innerText = stamp;

    function effectsLoaded(effects: string[]) {
        let html = "";
        for (let e of effects) 
            html += `<div class="program ${e}" onclick="commands.select(this)">${e}</div>`
        get("#programs").innerHTML = html;
    }

    sendWebRequest("/?method=effects", r => effectsLoaded(r.responseJSON));
    initControls();
    initPixels();
}