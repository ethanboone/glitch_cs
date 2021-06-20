<template>
  <tr class="text">
    <td>
      <router-link :to="{name: 'BugDetails', params: {id: bug.id}}">
        <div :class="{strike: bug.closed}" class="text">
          {{ bug.title }}
        </div>
      </router-link>
    </td>
    <td>
      <router-link :to="{name: 'BugDetails', params: {id: bug.id}}">
        <div class="text">
          <img :src="bug.creator.picture" height="35" alt="user icon"> {{ bug.creator.name }}
        </div>
      </router-link>
    </td>
    <td>
      <router-link :to="{name: 'BugDetails', params: {id: bug.id}}">
        <div class="text">
          {{ bug.closed }}
        </div>
      </router-link>
    </td>
    <td>
      <router-link :to="{name: 'BugDetails', params: {id: bug.id}}">
        <!-- This splits the time string into an array at the character T, and the first index of the array is what is being displayed -->
        <div class="text">
          {{ bug.updatedAt.split('T')[0] }}
        </div>
      </router-link>
    </td>
  </tr>
</template>

<script>
import { bugsService } from '../services/BugsService'
import { logger } from '../utils/Logger'
import { reactive, computed } from 'vue'
import { AppState } from '../AppState'

export default {
  name: 'Bugs',
  props: {
    bug: {
      type: Object,
      required: true
    }
  },
  setup() {
    const state = reactive({
      bugs: computed(() => AppState.bugs)
    })
    return {
      state,
      async getAll() {
        try {
          await bugsService.getAll()
        } catch (error) {
          logger.error(error)
        }
      }
    }
  }
}
</script>

<style scoped>
tr > td{
  border:2px solid #00ff00;
}

.text{
    color: #00ff00;
}

a:hover{
    text-decoration: none;
}

.strike{
    text-decoration: line-through;
}

</style>
