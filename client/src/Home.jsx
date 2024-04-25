import { getGreeting, getListOfDogs } from "./apiManager";
import { useEffect, useState } from "react";

export default function Home() {
  const [greeting, setGreeting] = useState({
    message: "Not Connected to the API",
  });
  const [dogs, setDogs] = useState([])

  useEffect(() => {
    getGreeting()
      .then(setGreeting)
      .catch(() => {
        console.log("API not connected");
      });
  }, []);

  useEffect(() => {
    getListOfDogs().then(data => {
      setDogs(data)
    })

  }, [])


  return (
   <div>
    <div>
      {dogs.map((dog) => {
        return (<div>{dog.name}</div>)
      })}
    </div>
   </div>
  )

}

