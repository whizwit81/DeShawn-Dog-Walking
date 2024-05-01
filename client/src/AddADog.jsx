import React, { useEffect, useState } from "react";
import { addNewDog, getListOfCities } from "./apiManager.js";
import { useNavigate } from "react-router-dom";
import { InputGroup } from "reactstrap";

export const AddDogForm = () => {
  const [newDog, setNewDog] = useState({ name: "", cityId: null });
  const [newSelectedCity, setNewSelectedCity] = useState([]);
  const [cities, setCities] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    getListOfCities().then((citiesList) => {
      setCities(citiesList);
    });
  }, []);

  const handleNewDogName = (e) => {
    const copy = { ...newDog };
    copy.name = e.target.value;
    setNewDog(copy);
  };

  const handleCityName = (e) => {
    const copy = {...newDog}
    copy.cityId = e.target.value;
    setNewDog(copy);
    setNewSelectedCity(e.target.value);
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    addNewDog(newDog).then((res) => {
      navigate(`/dogs/${res.id}`);
    });
  };

  return (
    <div>
      <form className="new-dog">
        <h1>Add a new dog</h1>

        <fieldset>
          <div>
            <label>New Dog Name:</label>
            <input type="text" onChange={handleNewDogName} />
          </div>
        </fieldset>
        <fieldset>
          <div>
            <div className="form-field">
              <p>Please select a city:</p>

              <label value="city" />
              <select
                id="city"
                value={newSelectedCity}
                onChange={handleCityName}
              >
                <option value="">Select a City</option>
                {cities.map((city) => (
                  <option key={city.id} value={city.id}>
                    {city.name}
                  </option>
                ))}
              </select>
            </div>
          </div>
        </fieldset>
      </form>
      <div className="button-field">
        <button
          className="submit_button"
          onClick={(e) => {
            handleSubmit(e);
          }}
        >
          Submit New Dog
        </button>
      </div>
    </div>
  );
};
