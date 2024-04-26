import { getGreeting, getListOfDogs } from "./apiManager";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";

export default function Home() {
  const [greeting, setGreeting] = useState({
    message: "Not Connected to the API",
  });
  const [dogs, setDogs] = useState([]);

  useEffect(() => {
    getGreeting()
      .then(setGreeting)
      .catch(() => {
        console.log("API not connected");
      });
  }, []);

  useEffect(() => {
    getListOfDogs().then((data) => {
      setDogs(data);
    });
  }, []);

  return (
    <div>
      <h1>{greeting.message}</h1>
      <div>
        {dogs.map((dog) => (
          <div key={dog.id}>
            <Link to={`/dogs/${dog.id}`}>{dog.name}</Link>
          </div>
        ))}
      </div>
    </div>
  );
}
