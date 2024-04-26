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

