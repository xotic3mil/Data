export async function fetchStatuses() {
  const response = await fetch("http://192.168.1.6:5000/api/status");
  if (!response.ok) throw new Error("Failed to fetch status");
  return response.json();
}

export async function fetchCustomerContact() {
  const response = await fetch("http://192.168.1.6:5000/api/contactperson");
  if (!response.ok) throw new Error("Failed to fetch customer contact persons");
  const data = await response.json();
  return data.map((cp) => ({
    ...cp,
    fullName: cp.firstName + " " + cp.lastName,
  }));
}

export async function fetchEmployees() {
  const response = await fetch("http://192.168.1.6:5000/api/employees");
  if (!response.ok) throw new Error("Failed to fetch employees");
  const data = await response.json();
  return data.map((emp) => ({
    ...emp,
    fullName: emp.firstName + " " + emp.lastName,
    email: emp.email,
  }));
}

export async function fetchServices() {
  const response = await fetch("http://192.168.1.6:5000/api/services");
  if (!response.ok) throw new Error("Failed to fetch services");
  return response.json();
}

export async function fetchCurrencies() {
  const response = await fetch("http://192.168.1.6:5000/api/currencies");
  if (!response.ok) throw new Error("Failed to fetch currencies");
  return response.json();
}

export async function fetchUnits() {
  const response = await fetch("http://192.168.1.6:5000/api/units");
  if (!response.ok) throw new Error("Failed to fetch units");
  return response.json();
}

export async function fetchCustomers() {
  const response = await fetch("http://192.168.1.6:5000/api/customers");
  if (!response.ok) throw new Error("Failed to fetch customers");
  return response.json();
}
