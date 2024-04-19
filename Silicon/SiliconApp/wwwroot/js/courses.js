const saveCourseWithFilters = (id) => {
    const categoryId = document.getElementById("category-selection");
    const search = document.getElementById("search-courses");

    fetch(`/courses/index?categoryId=${categoryId.value}&search=${search.value}&currentPage=${currentPage}`)
        .then(response => response.text()) //Responsen konverteras till text format (och detta �r ett promise, vilket g�r att vi m�ste g�ra �nnu en "then" f�r att manipulera den ytterligare)
        .then(data => {
            const parser = new DOMParser();
            const dom = parser.parseFromString(data, "text/html"); //Texten som vi fick ut fr�n responsen omvandlas till HTML-format med hj�lp av en DOMParser

            document.getElementById("courses-grid").innerHTML = dom.getElementById("courses-grid").innerHTML; //Inneh�llet p� sidan inuti courses-grid sektionen byts ut mot det som vi nu har f�tt ut
            document.getElementById("pagnation").innerHTML = dom.getElementById("pagnation").innerHTML;
        });
}