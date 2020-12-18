window.onload = function () {
    var formHandleTeacher = document.forms.updateformteacher;

    formHandleTeacher.onsubmit = processFormTeacher;

    function processFormTeacher() {
        var teacherfnameValue = document.getElementById("TeacherFname");
        var teacherlnameValue = document.getElementById("TeacherLname");
        var teachernumValue = document.getElementById("EmployeeNumber");
        var teachernumValue2 = document.getElementById("Salary");
        var teacherdateValue = document.getElementById("HireDate");


        if (teacherfnameValue.value === "" || teacherfnameValue.value === null) {
            teacherfnameValue.style.background = "red";
            teacherfnameValue.focus();
            return false;
        }
        else {
            teacherfnameValue.style.background = "white";
        }

        if (teacherlnameValue.value === "" || teacherlnameValue.value === null) {
            teacherlnameValue.style.background = "red";
            teacherlnameValue.focus();
            return false;
        }
        else {
            teacherlnameValue.style.background = "white";
        }

        if (teachernumValue.value === "" || teachernumValue.value === null) {
            teachernumValue.style.background = "red";
            teachernumValue.focus();
            return false;
        }
        else {
            teachernumValue.style.background = "white";
        }

        if (teachernumValue2.value === "" || teachernumValue2.value === null) {
            teachernumValue2.style.background = "red";
            teachernumValue2.focus();
            return false;
        }
        else {
            teachernumValue2.style.background = "white";
        }

        if (teacherdateValue.value === "" || teacherdateValue.value === null) {
            teacherdateValue.style.background = "red";
            teacherdateValue.focus();
            return false;
        }
        else {
            teacherdateValue.style.background = "white";
        }

    }
}