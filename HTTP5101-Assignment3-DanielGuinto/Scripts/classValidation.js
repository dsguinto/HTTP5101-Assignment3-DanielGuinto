window.onload = function () {

    var formHandleClass = document.forms.updateformclass;

    formHandleClass.onsubmit = processFormClass;

    function processFormClass() {
        var classnameValue = document.getElementById("ClassName");
        var classnumValue = document.getElementById("ClassCode");
        var classdateValue = document.getElementById("StartDate");
        var classdateValue2 = document.getElementById("FinishDate");


        if (classnameValue.value === "" || classnameValue.value === null) {
            classnameValue.style.background = "red";
            classnameValue.focus();
            return false;
        }
        else {
            classnameValue.style.background = "white";
        }

        if (classnumValue.value === "" || classnumValue.value === null) {
            classnumValue.style.background = "red";
            classnumValue.focus();
            return false;
        }
        else {
            classnumValue.style.background = "white";
        }

        if (classdateValue.value === "" || classdateValue.value === null) {
            classdateValue.style.background = "red";
            classdateValue.focus();
            return false;
        }
        else {
            classdateValue.style.background = "white";
        }

        if (classdateValue2.value === "" || classdateValue2.value === null) {
            classdateValue2.style.background = "red";
            classdateValue2.focus();
            return false;
        }
        else {
            classdateValue2.style.background = "white";
        }

    }
}