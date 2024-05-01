export const getGreeting = async () => {
  const res = await fetch("/api/hello");
  return res.json();
};

export const getListOfDogs = async () => {
  const res = await fetch("/api/dogs");
  return res.json();
};

export const getDogDetailsbyId = async (id) => {
  const res = await fetch(`/api/dogs/${id}`);
  return res.json();
};

export const getListOfCities = async () => {
  const res = await fetch("/api/cities");
  return res.json()
};

export const addNewDog = (dog) => {
  return fetch("/api/addadog", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(dog),
  }).then((res) => res.json());
};