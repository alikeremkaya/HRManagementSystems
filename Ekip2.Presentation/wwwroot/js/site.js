$(document).ready(function () {
    $('#mailSentPopup').modal('show');
});

//date card js
$(document).ready(function () {
    function formatDate(date) {
        var day = date.getDate();
        var month = date.getMonth() + 1; // January is 0!
        var year = date.getFullYear();
        if (day < 10) {
            day = '0' + day;
        }
        if (month < 10) {
            month = '0' + month;
        }
        return day + ' ' + month + ' ' + year;
    }
    var today = new Date();
    $('#currentDate').text(formatDate(today));
    $("#datepicker").datepicker({
        onSelect: function (dateText, inst) {
            $('#currentDate').text(dateText);
        }
    }).hide();
    $('#calendarButton').click(function () {
        $('#datepicker').toggle();
    });
});

function updateTime() {
    var now = new Date();
    var hours = now.getHours().toString().padStart(2, '0');
    var minutes = now.getMinutes().toString().padStart(2, '0');
    var seconds = now.getSeconds().toString().padStart(2, '0');
    var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    var months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    var dayName = days[now.getDay()];
    var day = now.getDate().toString().padStart(2, '0');
    var month = months[now.getMonth()];
    var year = now.getFullYear();
    var timeString = hours + ':' + minutes;
    var dateString = dayName + ', ' + day + ' ' + month + ' ' + year;
    document.getElementById('clock').innerText = timeString;
    document.getElementById('date').innerText = dateString;
}
setInterval(updateTime, 1000);
updateTime();


const leaveCounts = @Html.Raw(JsonConvert.SerializeObject(ViewBag.LeaveCounts));

const data = [
    { day: 'Pazartesi', value: leaveCounts["Monday"] },
    { day: 'Salı', value: leaveCounts["Tuesday"] },
    { day: 'Çarşamba', value: leaveCounts["Wednesday"] },
    { day: 'Perşembe', value: leaveCounts["Thursday"] },
    { day: 'Cuma', value: leaveCounts["Friday"] },
    { day: 'Cumartesi', value: leaveCounts["Saturday"] },
    { day: 'Pazar', value: leaveCounts["Sunday"] }
];

const labels = data.map(item => item.day);
const values = data.map(item => item.value);

const dataContainer = document.getElementById('dataContainer');
const leaveCounts = JSON.parse(dataContainer.getAttribute('data-leave-counts'));
const advanceCounts = JSON.parse(dataContainer.getAttribute('data-advance-counts'));
const expenseCounts = JSON.parse(dataContainer.getAttribute('data-expense-counts'));


// Bar Chart oluşturma
const ctx = document.getElementById('barChart').getContext('2d');
new Chart(ctx, {
    type: 'bar',
    data: {
        labels: labels,
        datasets: [{
            label: 'Daily Values',
            data: values,
            backgroundColor: 'rgba(75, 192, 192, 0.2)',
            borderColor: 'rgba(75, 192, 192, 1)',
            borderWidth: 1
        }]
    },
    options: {
        scales: {
            x: {
                beginAtZero: true
            },
            y: {
                beginAtZero: true
            }
        }
    }
});

const advanceCounts = @Html.Raw(JsonConvert.SerializeObject(ViewBag.AdvanceCounts));

const advanceData = [
    { day: 'Pazartesi', value: advanceCounts["Monday"] },
    { day: 'Salı', value: advanceCounts["Tuesday"] },
    { day: 'Çarşamba', value: advanceCounts["Wednesday"] },
    { day: 'Perşembe', value: advanceCounts["Thursday"] },
    { day: 'Cuma', value: advanceCounts["Friday"] },
    { day: 'Cumartesi', value: advanceCounts["Saturday"] },
    { day: 'Pazar', value: advanceCounts["Sunday"] }
];

const advanceLabels = advanceData.map(item => item.day);
const advanceValues = advanceData.map(item => item.value);

// Advance Bar Chart oluşturma
const ctx2 = document.getElementById('barChart2').getContext('2d');
new Chart(ctx2, {
    type: 'line',
    data: {
        labels: advanceLabels,
        datasets: [{
            label: 'Günlük Değerler',
            data: advanceValues,
            backgroundColor: 'rgba(153, 102, 255, 0.2)',
            borderColor: 'rgba(153, 102, 255, 1)',
            borderWidth: 1,
            fill: true,
            lineTension: 0.2
        }]
    },
    options: {
        scales: {
            x: {
                beginAtZero: true
            },
            y: {
                beginAtZero: true
            }
        }
    }
});

const expenseCounts = @Html.Raw(JsonConvert.SerializeObject(ViewBag.ExpenseCounts));

const expenseData = [
    { day: 'Pazartesi', value: expenseCounts["Monday"] },
    { day: 'Salı', value: expenseCounts["Tuesday"] },
    { day: 'Çarşamba', value: expenseCounts["Wednesday"] },
    { day: 'Perşembe', value: expenseCounts["Thursday"] },
    { day: 'Cuma', value: expenseCounts["Friday"] },
    { day: 'Cumartesi', value: expenseCounts["Saturday"] },
    { day: 'Pazar', value: expenseCounts["Sunday"] }
];

const labels = expenseData.map(item => item.day);
const values = expenseData.map(item => item.value);

// Line Chart oluşturma
const ctx = document.getElementById('lineChart').getContext('2d');
new Chart(ctx, {
    type: 'line',
    data: {
        labels: labels,
        datasets: [{
            label: 'Weekly Expenses',
            data: values,
            backgroundColor: 'rgba(153, 102, 255, 0.2)',
            borderColor: 'rgba(153, 102, 255, 1)',
            borderWidth: 2,
            fill: true,
            lineTension: 0.2
        }]
    }
});
//hava durumu api
const apiKey = '1519087dc3ada1e87078f3ac8309d2bb';
const url = 'https://api.openweathermap.org/data/2.5/weather';


const setQuery = (e) => {
    if (e.keyCode === 13) { // enter tuşu
        getResult(searchBar.value);
    }
};

// search
const getResult = (cityName) => {
    let query = `${url}?q=${cityName}&appid=${apiKey}&units=metric&lang=tr`;
    fetch(query)
        .then(response => response.json())
        .then(displayResult)
        .catch(error => {
            console.error("API Error:", error);
            document.querySelector('.city').innerText = 'Hava durumu bilgisi alınamadı.';
            document.querySelector('.temp').innerText = '';
            document.querySelector('.desc').innerText = '';
            document.querySelector('.minmax').innerText = '';
        });
};


const displayResult = (result) => {
    let city = document.querySelector('.city');
    city.innerText = `${result.name}, ${result.sys.country}`;

    let temp = document.querySelector('.temp');
    temp.innerText = `${Math.round(result.main.temp)}°C`;

    let desc = document.querySelector('.desc');
    desc.innerText = result.weather[0].description.charAt(0).toUpperCase() + result.weather[0].description.slice(1);

    let minmax = document.querySelector('.minmax');
    minmax.innerText = `${Math.round(result.main.temp_min)}/ ${Math.round(result.main.temp_max)}°C`;
};


const searchBar = document.getElementById('searchBar');
searchBar.addEventListener('keypress', setQuery);