import { createApp } from "vue";
import { createRouter, createWebHistory } from "vue-router";
import App from "./App.vue";
import store from "./store/index";
import primeVue from "primevue/config";
import "primevue/resources/themes/lara-light-indigo/theme.css";
import "primevue/resources/primevue.min.css";
import "primeflex/primeflex.css";

const Home = () => import("./components/UserMainPageComponent.vue");
const Login = () => import("./components/Login/LoginPageComponent.vue");
const Appointments = () =>
  import("./components/Appointment/AppointmentsComponent.vue");
const Appointment = () =>
  import("./components/Appointment/AppointmentComponent.vue");
const YourPets = () => import("./components/Pets/YourPetsComponent.vue");
const AddPet = () => import("./components/Pets/AddPetComponent.vue");
const AddAppointment = () =>
  import("./components/Appointment/AddAppointmentComponent.vue");

const routes = [
  {
    path: "/",
    name: "home",
    component: Home,
    meta: { requiresAuth: true },
  },
  {
    path: "/login",
    name: "Login",
    component: Login,
    meta: { requiresAuth: false },
  },
  {
    path: "/yourAppointments",
    name: "Appointments",
    component: Appointments,
    meta: { requiresAuth: true },
  },
  ,
  {
    path: "/yourPets",
    name: "yourPets",
    component: YourPets,
    meta: { requiresAuth: true },
  },
  {
    path: "/addNewPet",
    name: "addNewPet",
    component: AddPet,
    meta: { requiresAuth: true },
  },
  {
    path: "/addNewAppointment",
    name: "addNewAppointment",
    component: AddAppointment,
    meta: { requiresAuth: true },
  },
  {
    path: "/appointment/:visitId",
    name: "appointment",
    component: Appointment,
    meta: { requiresAuth: true },
  },
  ,
  {
    path: "/newAppointment",
    name: "newAppointment",
    component: Appointment,
    meta: { requiresAuth: true },
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  const isAuthenticated = localStorage.getItem("token") != null;
  if (to.meta.requiresAuth && !isAuthenticated) {
    next("/login");
  } else {
    next();
  }
});

const app = createApp(App);

app.use(router);

app.use(store);

app.use(primeVue);

app.mount("#app");
