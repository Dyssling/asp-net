let sw = document.getElementById("theme-switch");               //Hämtar både desktop och mobil switcharna
let swMobile = document.getElementById("theme-switch-mobile");

sw.addEventListener("change", () => { //Lägg till en event listener på desktop switchen, som kör funktionen när värdet ändras
    let theme = sw.checked ? "dark" : "light"; //Om den är bockad så sätts theme variabeln till dark, annars light

    fetch(`/sitesettings/changetheme?theme=${theme}`) //En fetch utförs, som alltså kommer hämta en respons från följande URL, vilket i detta fall innebär att ChangeTheme action kommer köras, i SiteSettings controllern
        .then(response => { //En fetch är asynkron, men NÄR den är färdig så vill vi göra en check på responsen
            if (response.ok) {
                window.location.reload(); //Om allting lyckades, och vi får tillbaka en Ok respons från ChangeTheme, så laddas sidan om
            }
        });
});

swMobile.addEventListener("change", () => {
    let theme = swMobile.checked ? "dark" : "light";

    fetch(`/sitesettings/changetheme?theme=${theme}`)
        .then(response => {
            if (response.ok) {
                window.location.reload();
            }
        });
});


const toggleMenu = () => {
    if (document.getElementById("menu").style.display != "block") {
        document.getElementById("menu").style.display = "block";
    }

    else {
        document.getElementById("menu").style.display = "none";
    }
};
