window.onload = function () {

    var formHandleStudent = document.forms.updateformstudent;

    formHandleStudent.onsubmit = processFormStudent;

    function processFormStudent() {
        var studentfnameValue = document.getElementById("StudentFname");
        var studentlnameValue = document.getElementById("StudentLname");
        var studentnumValue = document.getElementById("StudentNumber");
        var studentdateValue = document.getElementById("EnrolDate");


        if (studentfnameValue.value === "" || studentfnameValue.value === null) {
            studentfnameValue.style.background = "red";
            studentfnameValue.focus();
            return false;
        }
        else {
            studentfnameValue.style.background = "white";
        }

        if (studentlnameValue.value === "" || studentlnameValue.value === null) {
            studentlnameValue.style.background = "red";
            studentlnameValue.focus();
            return false;
        }
        else {
            studentlnameValue.style.background = "white";
        }

        if (studentnumValue.value === "" || studentnumValue.value === null) {
            studentnumValue.style.background = "red";
            studentnumValue.focus();
            return false;
        }
        else {
            studentnumValue.style.background = "white";
        }

        if (studentdateValue.value === "" || studentdateValue.value === null) {
            studentdateValue.style.background = "red";
            studentdateValue.focus();
            return false;
        }
        else {
            studentdateValue.style.background = "white";
        }
    }
}