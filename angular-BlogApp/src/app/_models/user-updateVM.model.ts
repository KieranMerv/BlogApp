export interface UserUpdateVM {
  userName: string;
  email: string;
  newEmail: string;
  password: string;
  newPassword: string | null;
  alias: string;
}