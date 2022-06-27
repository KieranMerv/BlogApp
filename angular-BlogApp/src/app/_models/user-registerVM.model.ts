export interface UserRegisterVM {
  userName: string;
  alias: string | null;
  email: string;
  password: string;
  confirmPassword: string;
}