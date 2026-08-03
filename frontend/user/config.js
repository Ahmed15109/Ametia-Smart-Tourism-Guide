// Override window.AMETIA_API_BASE_URL before this script loads when hosting elsewhere.
const API_BASE_URL = window.AMETIA_API_BASE_URL || "https://localhost:7124/api";

// Existing pages use this alias; keep it centralized here to preserve their behavior.
const baseURL = API_BASE_URL;

let cityLookupPromise;

function loadCityLookup() {
  if (!cityLookupPromise) {
    cityLookupPromise = fetch(`${API_BASE_URL}/City/GetCity`)
      .then(response => {
        if (!response.ok) throw new Error("Failed to load cities.");
        return response.json();
      })
      .then(cities => Object.fromEntries(cities.map(city => [String(city.id), city.name])));
  }

  return cityLookupPromise;
}

function resolveCityName(cityLookup, cityId) {
  return cityLookup[String(cityId)] || "N/A";
}
