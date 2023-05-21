import axios from "axios";

const http = axios.create({
  baseURL: "https://localhost:7002/",
});

export const get = async (url, config = {}) => {
  const token = localStorage.getItem("token");
  http.defaults.headers.common["Authorization"] = `Bearer ${token}`;
  const response = await http.get(url, config);
  return response.data;
};

export const post = async (url, data, config = {}) => {
  const token = localStorage.getItem("token");
  http.defaults.headers.common["Authorization"] = `Bearer ${token}`;
  const response = await http.post(url, data, config);
  return response.data;
};
