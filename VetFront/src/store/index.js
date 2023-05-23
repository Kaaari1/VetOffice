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
      localStorage.clear();
    },
  },
});

export default store;
