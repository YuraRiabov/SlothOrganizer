import { Token } from '#types/auth/token';

export const getDefaultToken = () : Token => ({
    accessToken: '',
    refreshToken: ''
});