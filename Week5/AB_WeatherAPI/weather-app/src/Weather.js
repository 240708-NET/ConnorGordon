import React, { useEffect, useState } from 'react';
import axios from 'axios';

const Weather = () => {
    const [city, setCity] = useState('');
    const [weatherData, setWeatherData] = useState(null);

    const fetchData = async () => {
        try {
            const response = await axios.get(`https://freetestapi.com/api/v1/weathers?search=${city}`);
            //const response = await axios.get(`https://api.openweathermap.org/data/2.5/weather?q=${city}&units=metric&appid={f3373e687411258e9c37b287037069f9}`);
            setWeatherData(response.data);
            console.log(response.data); //You can see all the weather data in console log
        } 
        
        catch (error) {
            console.error(error);
        }
    };

    /*
    useEffect(() => {
        fetchData();
    }, []);
    */

    const handleInputChange = (e) => {
        setCity(e.target.value);
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        fetchData();
    };

    return (
        <div id="data">
        <form onSubmit={handleSubmit}>
            <input id="cityField" type="text" placeholder="Enter city name" value={city} onChange={handleInputChange} />
            <button id="citySubmit"type="submit">Get Weather</button>
        </form>
        {weatherData && city.toLowerCase() == weatherData[0].city.toLowerCase() ? (
            <>
            <h2>{weatherData[0].city}</h2>
            <p>Temperature: {weatherData[0].temperature}°C</p>
            <p>Description: {weatherData[0].weather_description}</p>
            <p>Humidity: {weatherData[0].humidity}%</p>
            <p>Wind Speed: {weatherData[0].wind_speed}m/s</p>
            </>
        ) : (
            <p>Loading weather data...</p>
        )}
        </div>
    );
};

export default Weather;