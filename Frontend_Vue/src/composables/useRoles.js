//// filepath: /d:/Github/Data/Frontend_Vue/src/composables/useRoles.js
import { ref, onMounted } from "vue";
import { fetchRoles } from "@/endpoints/apiService.js";

export function useRoles() {
  const roles = ref([]);
  const loading = ref(false);
  const error = ref(null);

  async function loadRoles() {
    loading.value = true;
    error.value = null;
    try {
      roles.value = await fetchRoles();
    } catch (err) {
      error.value = err;
      console.error("Error fetching roles:", err);
    } finally {
      loading.value = false;
    }
  }

  onMounted(loadRoles);

  return { roles, loading, error, loadRoles };
}
