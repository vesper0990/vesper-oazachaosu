function saveGroup(element) {
    var line = $(element).closest('tr');
    var group = [
        {
            Id: line.find('.groupId').html(),
            Name: line.find('input[name=groupName]').val(),
            Language1: parseInt(line.find('input[name=language1Type]').val()),
            Language2: parseInt(line.find('input[name=language2Type]').val()),
            State: 2147483647,
        }];
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            console.log(xhttp.responseText);
            var response = JSON.parse(xhttp.responseText);
            if (!response.IsError) {
                $("#modalMessage").html("Zapisano");
            } else {
                $("#modalMessage").html("Błąd zapisu: " + response.Message);
            }
        }
    };
    xhttp.open("PUT", generateApiUrl('group'), true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.setRequestHeader("authorization", getCookie('authorization'));
    console.log(JSON.stringify(group));
    xhttp.send(JSON.stringify(group));
    $("#modalMessage").html("Zapisuje...");
}

function deleteGroup(element) {
    var line = $(element).closest('tr');
    var group = [{
        Id: line.find('.groupId').html(),
    }];
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        console.log(xhttp.responseText);
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            var response = JSON.parse(xhttp.responseText);
            if (!response.IsError) {
                $("#modalMessage").html("Usunięto");
                line.remove();
            } else {
                $("#modalMessage").html("Błąd usuwania: " + response.Message);
            }
        }
    };
    xhttp.open("DELETE", generateApiUrl('group'), true);
    xhttp.setRequestHeader("Content-type", "application/json;charset=UTF-8");
    xhttp.setRequestHeader("authorization", getCookie('authorization'));
    console.log(JSON.stringify(group));
    xhttp.send(JSON.stringify(group));
    $("#modalMessage").html("Usuwam...");
}

function addGroup(element) {
    var line = $(element).closest('tr');
    var group = [
        {
            Name: line.find('input[name=groupName]').val(),
            Language1: parseInt(line.find('input[name=language1Type]').val()),
            Language2: parseInt(line.find('input[name=language2Type]').val()),
            State: 2147483647
        }];
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            var response = JSON.parse(xhttp.responseText);
            if (!response.IsError) {
                location.reload();
            } else {
                $("#modalMessage").html("Błąd dodawania: " + response.Message);
            }
        }
    };
    var url = generateApiUrl('group');
    xhttp.open("POST", url, true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.setRequestHeader("authorization", getCookie('authorization'));
    var jsonObject = JSON.stringify(group);
    xhttp.send(jsonObject);
    $("#modalMessage").html("Dodaje...");
}

function saveWord(element) {
    var line = $(element).closest('tr');
    var word = [{
        Id: line.find('.wordId').html(),
        GroupId: getCookie('groupId'),
        Language1: line.find('input[name=language1]').val(),
        Language2: line.find('input[name=language2]').val(),
        Drawer: line.find('.drawer').html(),
        Visible: line.find('input[type=checkbox]').prop('checked'),
        State: 2147483647
    }];
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            var response = JSON.parse(xhttp.responseText);
            if (!response.IsError) {
                $("#modalMessage").html("Zapisano");
            } else {
                $("#modalMessage").html("Błąd zapisu: " + response.Message);
            }
        }
    };
    xhttp.open("PUT", generateApiUrl('word'), true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.setRequestHeader("authorization", getCookie('authorization'));
    console.log(JSON.stringify(word));
    xhttp.send(JSON.stringify(word));
}


function addWord(element) {
    var line = $(element).closest('tr');
    var word = [
        {
            groupId: getCookie('groupId'),
            language1: line.find('input[name=language1]').val(),
            language2: line.find('input[name=language2]').val()
        }];
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            var response = JSON.parse(xhttp.responseText);
            if (!response.error) {
                location.reload();
            } else {
                alert(xhttp.responseText);
                var color = "red";
                line.stop().animate({ "background-color": color }, 'fast').animate({ "background-color": "transparent" }, 'fast');
            }
        }
    };
    xhttp.open("POST", generateApiUrl('word'), true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.setRequestHeader("authorization", getCookie('authorization'));
    console.log(JSON.stringify(word));
    xhttp.send(JSON.stringify(word));
}


function deleteWord(element) {
    var line = $(element).closest('tr');
    var word = [
        {
            userId: getCookie('userId'),
            Id: line.find('.wordId').html()
        }];
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            var response = JSON.parse(xhttp.responseText);
            if (!response.error) {
                line.remove();
            } else {
                console.log(xhttp.responseText);
            }
        }
    };
    xhttp.open("DELETE", generateApiUrl('word'), true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.setRequestHeader("authorization", getCookie('authorization'));
    console.log(JSON.stringify(word));
    xhttp.send(JSON.stringify(word));
}

function saveCommonGroup(element) {
    var line = $(element).closest('tr');
    var group = [{
        groupId: line.find('.groupId').html(),
        groupName: line.find('input[name=groupName]').val(),
        language1Type: line.find('input[name=language1Type]').val(),
        language2Type: line.find('input[name=language2Type]').val(),
        state: 3
    }];
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            var response = JSON.parse(xhttp.responseText);
            if (!response.error) {
                location.reload();
            } else {
                console.log(xhttp.responseText);
                line.stop().animate({ "background-color": "red" }, 'fast').animate({ "background-color": "transparent" }, 'fast');
            }
        }
    };
    xhttp.open("PUT", generateApiUrl('commonGroups'), true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.setRequestHeader("authorization", getCookie('authorization'));
    console.log(JSON.stringify(group));
    xhttp.send("commonGroups=" + JSON.stringify(group));
}

function addCommonGroup(element) {
    var line = $(element).closest('tr');
    var group = [{
        groupName: line.find('input[name=groupName]').val(),
        language1Type: line.find('input[name=language1Type]').val(),
        language2Type: line.find('input[name=language2Type]').val(),
        state: 3
    }];
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            var response = JSON.parse(xhttp.responseText);
            if (!response.error) {
                location.reload();
            } else {
                console.log(xhttp.responseText);
                line.stop().animate({ "background-color": "red" }, 'fast').animate({ "background-color": "transparent" }, 'fast');
            }
        }
    };
    xhttp.open("POST", generateApiUrl('commonGroups'), true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.setRequestHeader("authorization", getCookie('authorization'));
    console.log(JSON.stringify(group));
    xhttp.send(JSON.stringify(group));
}

function deleteCommonGroup(element) {
    var line = $(element).closest('tr');
    var group = [
        {
            groupId: line.find('.groupId').html()
        }];
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            var response = JSON.parse(xhttp.responseText);
            if (!response.error) {
                line.remove();
            } else {
                console.log(xhttp.responseText);
                line.stop().animate({ "background-color": "red" }, 'fast').animate({ "background-color": "transparent" }, 'fast');
            }
        }
    };
    xhttp.open("POST", generateApiUrl('commonGroups'), true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.setRequestHeader("authorization", getCookie('authorization'));
    console.log(JSON.stringify(group));
    xhttp.send(JSON.stringify(group));
}

function addCommonWord(element) {
    var line = $(element).closest('tr');
    groupId = _GET('groupId');
    language1 = line.find('input[name=language1]').val();
    language2 = line.find('input[name=language2]').val();
    var word = [{
        groupId: _GET('groupId'),
        language1: line.find('input[name=language1]').val(),
        language2: line.find('input[name=language2]').val()
    }];
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            var response = JSON.parse(xhttp.responseText);
            if (!response.error) {
                location.reload();
            } else {
                alert(xhttp.responseText);
                line.stop().animate({ "background-color": "red" }, 'fast').animate({ "background-color": "transparent" }, 'fast');
            }
        }
    };
    xhttp.open("POST", generateApiUrl('commonWords'), true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.setRequestHeader("authorization", getCookie('authorization'));
    console.log(JSON.stringify(word));
    xhttp.send(JSON.stringify(word));
}

function saveCommonWord(element) {
    line = $(element).closest('tr');
    groupId = _GET('groupId');
    wordId = line.find('.wordId').html();
    language1 = line.find('input[name=language1]').val();
    language2 = line.find('input[name=language2]').val();
    drawer = line.find('.drawer').html();
    visible = line.find('input[type=checkbox]').prop('checked') ? 1 : 0;
    word = [{ wordId: wordId, groupId: groupId, language1: language1, language2: language2, state: 3 }];
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            response = JSON.parse(xhttp.responseText);
            color = null;
            if (!response.error) {
                color = "green";
            } else {
                console.log(xhttp.responseText);
                color = "red";
            }
            line.stop().animate({ "background-color": color }, 'fast').animate({ "background-color": "transparent" }, 'fast');
        }
    };
    xhttp.open("PUT", generateApiUrl('commonWords'), true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.setRequestHeader("authorization", getCookie('authorization'));
    console.log("commonWords=" + JSON.stringify(word));
    xhttp.send("commonWords=" + JSON.stringify(word));
}

function deleteCommonWord(element) {
    line = $(element).closest('tr');
    wordId = line.find('.wordId').html();
    groupId = _GET('groupId');
    word = [{ wordId: wordId, groupId: groupId }];
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            response = JSON.parse(xhttp.responseText);
            if (!response.error) {
                line.remove();
            } else {
                console.log(xhttp.responseText);
            }
        }
    };
    xhttp.open("DELETE", generateApiUrl('commonWords'), true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.setRequestHeader("authorization", getCookie('authorization'));
    console.log(JSON.stringify(word));
    xhttp.send("commonWords=" + JSON.stringify(word));
}

function avoidWord(element) {
    $(element).closest('tr').remove();
}

function createCommonGroup(element) {
    var groupName = getCookie('groupName');
    var group = [{ groupName: groupName, language1Type: 0, language2Type: 0, state: 3 }];
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            console.log(xhttp.responseText);
            var response = JSON.parse(xhttp.responseText);
            if (!response.error) {
                var groupId = response.message;
                console.log(groupId);
                var words = [];
                var table = document.getElementById("newGroupTable");
                for (i = 1; row = table.rows[i]; i++) {
                    words.push({
                        groupId: groupId,
                        language1: $(row).find('input[name=language1]').val(),
                        language2: $(row).find('input[name=language2]').val()
                    });
                }
                xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = function () {
                    if (xhttp.readyState === 4 && xhttp.status === 200) {
                        response = JSON.parse(xhttp.responseText);
                        if (!response.error) {
                            alert('Stworzono');
                        } else {
                            alert(xhttp.responseText);
                        }
                    }
                };
                xhttp.open("POST", generateApiUrl('commonWords'), true);
                xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
                xhttp.setRequestHeader("authorization", getCookie('authorization'));
                console.log("commonWords=" + JSON.stringify(words));
                xhttp.send("commonWords=" + JSON.stringify(words));

            } else {
                console.log(xhttp.responseText);
            }
        }
    };
    xhttp.open("POST", generateApiUrl('commonGroups'), true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.setRequestHeader("authorization", getCookie('authorization'));
    console.log(JSON.stringify(group));
    xhttp.send("commonGroups=" + JSON.stringify(group));
}


function createGroup() {
    var userId = getCookie('userId');
    var name = getCookie('groupName');
    var group = [
        {
            UserId: userId,
            Name: name,
            Language1: 0,
            Language2: 0,
        }];
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            console.log(xhttp.responseText);
            var response = JSON.parse(xhttp.responseText);
            if (!response.IsError) {
                var groupId = response.Message;
                console.log(groupId);
                console.log(userId);
                var words = [];
                var table = document.getElementById("newGroupTable");
                var row;
                var i;
                for (i = 1; row = table.rows[i]; i++) {
                    words.push({
                        userId: userId,
                        groupId: groupId,
                        language1: $(row).find('input[name=language1]').val(),
                        language2: $(row).find('input[name=language2]').val()
                    });
                }
                xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = function () {
                    if (xhttp.readyState === 4 && xhttp.status === 200) {
                        response = JSON.parse(xhttp.responseText);
                        if (!response.error) {
                            alert('Stworzono');
                            window.location.replace(generateUrl('wordki/1'));
                        } else {
                            alert(xhttp.responseText);
                        }
                    }
                };
                xhttp.open("POST", generateApiUrl('word'), true);
                xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
                xhttp.setRequestHeader("authorization", getCookie('authorization'));
                console.log(JSON.stringify(words));
                xhttp.send(JSON.stringify(words));

            } else {
                console.log(xhttp.responseText);
            }
        }
    };
    xhttp.open("POST", generateApiUrl('group'), true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.setRequestHeader("authorization", getCookie('authorization'));
    console.log(JSON.stringify(group));
    xhttp.send(JSON.stringify(group));
}


function addGroupToUse(element) {
    line = $(element).closest('tr');
    groupId = line.find('.groupId').html();
    group = [{ groupId: groupId }];
    xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState === 4 && xhttp.status === 200) {
            response = JSON.parse(xhttp.responseText);
            if (!response.error) {
                alert('Pobrano');
            } else {
                console.log(xhttp.responseText);
                color = "red";
                line.stop().animate({ "background-color": color }, 'fast').animate({ "background-color": "transparent" }, 'fast');
            }
        }
    };
    xhttp.open("POST", generateApiUrl('addGroupToUse'), true);
    xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhttp.setRequestHeader("authorization", getCookie('authorization'));
    console.log(JSON.stringify(group));
    xhttp.send("commonGroups=" + JSON.stringify(group));
}

function addProcessingElement(element) {
    $(element).closest(".actions").append('<img class="processing_gif" src="' + generateUrl('Images/processing.gif') + '" alt="processing.gif"/>');
}

function removeProccessingElement(element) {
    $(element).find('.processing_gif').remove();
}
