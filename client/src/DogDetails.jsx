import React, { useEffect, useState } from "react";
import { getDogDetailsbyId } from "./apiManager";
import { useParams } from "react-router-dom";

const DogDetails = () => {
  const [dog, setDog] = useState({});
  const { id } = useParams();

  useEffect(() => {
        getDogDetailsbyId(id).then((dog) =>
        setDog(dog))
  
  }, [id]);

  return (
    <div>
        <div>
          <h2>{dog.name}</h2>
          <p>{dog.name} resides in {dog.city?.name}  and {dog.walkers ? `is being walked by ${dog.walkers?.name}.` : "does not currently have a walker assinged."} </p>
        
        </div>
    </div>
  );
};

export default DogDetails


