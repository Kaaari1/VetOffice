import { createStore } from "vuex";

const store = createStore({
  state: {
    isAuthenticated: false,
  },
  actions: {
    login(skip, data) {
      localStorage.setItem("token", data.token);
      localStorage.setItem("userId", data.userId);
      localStorage.setItem("role", data.role);
    },
    logout() {
      localStorage.setItem("userId", null);
      localStorage.setItem("role", null);
      localStorage.setItem("token", null);
    },
  },
});

export default store;
