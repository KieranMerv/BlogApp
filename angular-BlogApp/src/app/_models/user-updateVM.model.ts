export interface UserUpdateVM {
  userName: string;
  alias: string;
  email: string;
  newEmail: string | null;
  password: string;
  newPassword: string | null;
  newConfirmPassword: string | null;
}