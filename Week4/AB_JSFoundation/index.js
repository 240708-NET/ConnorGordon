const value1 = document.querySelector('#value1');
const value2 = document.querySelector('#value2');

document.getElementById('num1').addEventListener('click', appendNum);
document.getElementById('num2').addEventListener('click', appendNum);
document.getElementById('num3').addEventListener('click', appendNum);
document.getElementById('num4').addEventListener('click', appendNum);
document.getElementById('num5').addEventListener('click', appendNum);
document.getElementById('num6').addEventListener('click', appendNum);
document.getElementById('num7').addEventListener('click', appendNum);
document.getElementById('num8').addEventListener('click', appendNum);
document.getElementById('num9').addEventListener('click', appendNum);
document.getElementById('num0').addEventListener('click', appendNum);

document.getElementById('add').addEventListener('click', addNum);
document.getElementById('sub').addEventListener('click', subNum);
document.getElementById('mlt').addEventListener('click', mltNum);

function appendNum(e) {
    value2.append(e.currentTarget.value);
}

function addNum(e) {
    if (value1.innerHTML == "") {
        value1.append(value2.innerHTML);
        value2.innerHTML = "";
    }
    else {
        value1.innerHTML = (Number(value1.innerHTML) + Number(value2.innerHTML));
        value2.innerHTML = "";
    }
}

function subNum(e) {
    if (value1.innerHTML == "") {
        value1.append(value2.innerHTML);
        value2.innerHTML = "";
    }
    else {
        value1.innerHTML = (Number(value1.innerHTML) - Number(value2.innerHTML));
        value2.innerHTML = "";
    }
}

function mltNum(e) {
    if (value1.innerHTML == "") {
        value1.append(value2.innerHTML);
        value2.innerHTML = "";
    }
    else {
        value1.innerHTML = (Number(value1.innerHTML) * Number(value2.innerHTML));
        value2.innerHTML = "";
    }
}