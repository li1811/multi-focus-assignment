

document.getElementById("submit-button").addEventListener("click", () => {
    const startUnit = document.getElementById("from-select").value;
    const endUnit = document.getElementById("to-select").value;
    const temperatureToConvert = document.getElementById("input-number").value;
    // Call the temperatureConvert function with the fetched values
    convertTemperature(startUnit, endUnit, temperatureToConvert);
  });

function convertTemperature(inputUnit, outputUnit, temperature) {
    const formData = new FormData;
    formData.append("input", temperature)
    
    fetch(`http://localhost:5065/${inputUnit}to${outputUnit}`, {
        method: "POST",
        body: formData,
    })
    .then((response) => {
        if (!response.ok) {
            throw new Error("failed fetch from backend");
        }
        return response.json();
    })
    .then ((data) => {
        console.log(data);
        document.getElementById("convertedData").innerText = data;
    })
    .catch ((error) => {
        console.error("error", error)
    })
}

//convertTemperature("celsius", "celsius", 10);
//convertTemperature("celsius", "fahrenheit", 10);
//convertTemperature("celsius", "kelvin", 10);
//convertTemperature("fahrenheit", "fahrenheit", 10);
//convertTemperature("fahrenheit", "celsius", 10);
//convertTemperature("fahrenheit", "kelvin", 10);
//convertTemperature("kelvin", "kelvin", 10);
//convertTemperature("kelvin", "celsius", 10);
//convertTemperature("kelvin", "fahrenheit", 10);