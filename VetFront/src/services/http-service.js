import axios from 'axios';

export const createHttpService = function (config) {
    
    const newHttpService = axios.create(config);

    newHttpService.resolveUrl = (url) => {
        return `${newHttpService.defaults.baseURL}${url}`;
    };

    newHttpService.setBaseUrl = (baseUrl) => {
        newHttpService.defaults.baseURL = baseUrl;
    };

    return newHttpService;
}

const httpService = createHttpService({
    baseURL: "https://localhost:7002/",
    withCredentials: false
});

export default httpService;