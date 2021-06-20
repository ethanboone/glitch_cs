<template>
  <div>
    <div class="d-flex row justify-content-between my-3">
      <div class="col-5 col-md-3 d-flex align-items-center">
        <h2 class="text mb-0 py-0 d-flex align-items-center">
          Bugs
        </h2>
        <div class="d-flex">
          <button class="btn btn-outline-green text d-flex mx-3" data-toggle="modal" data-target="#bug-modal" v-if="state.user.isAuthenticated">
            +
          </button>
        </div>
      </div>
      <div class="col-5 col-md-3 d-flex justify-content-center">
        <button class="btn btn-outline-green text" @click="sort">
          Open Bugs Only
        </button>
      </div>
    </div>
    <div class="modal" id="bug-modal" tabindex="-1">
      <div class="modal-dialog">
        <div class="modal-content bg-dark">
          <div class="modal-header bg-dark">
            <h5 class="modal-title text-light">
              New Note
            </h5>
          </div>
          <div class="modal-body bg-dark">
            <form class="form-group" @submit.prevent="create">
              <label class="text-light">Name</label>
              <input type="text" class="form-control" v-model="state.newBug.title" required>
              <label class="text-light">Description</label>
              <input type="text" class="form-control" v-model="state.newBug.description" required>
              <button type="button" class="btn btn-outline-danger text-danger" data-dismiss="modal">
                Close
              </button>
              <button type="submit" class="btn btn-outline-primary text-primary">
                Create
              </button>
            </form>
          </div>
        </div>
      </div>
    </div>
    <table class="table table-bordered border-success">
      <thead>
        <tr class="text">
          <th scope="col">
            Title
          </th>
          <th scope="col">
            Created By
          </th>
          <th scope="col">
            Closed
          </th>
          <th scope="col">
            Last Updated
          </th>
        </tr>
      </thead>
      <!-- these are two separate loops. One is for if the user wants to see bugs that aren't closed,
      and the other is for all bugs including the closed ones -->
      <tbody v-if="state.sort">
        <Bugs v-for="bug in state.openBugs" :bug="bug" :key="bug.id" />
      </tbody>
      <tbody v-else>
        <Bugs v-for="bug in state.bugs" :bug="bug" :key="bug.id" />
      </tbody>
    </table>
  </div>
</template>

<script>
import { reactive, onMounted, computed } from 'vue'
import { AppState } from '../AppState'
import { bugsService } from '../services/BugsService'
import { logger } from '../utils/Logger'
import $ from 'jquery'
import { useRouter } from 'vue-router'

export default {
  name: 'Home',
  setup() {
    const router = useRouter()
    const state = reactive({
      user: computed(() => AppState.user),
      bugs: computed(() => AppState.bugs),
      openBugs: computed(() => AppState.bugs.filter(b => b.closed === false)),
      newBug: {},
      sort: false
    })
    onMounted(async() => {
      bugsService.getAll()
    })
    return {
      state,
      async create() {
        try {
          const res = await bugsService.create(state.newBug)
          // router.push to route to details page
          router.push({ name: 'BugDetails', params: { id: res } })
          $('#bug-modal').modal('hide')
        } catch (error) {
          logger.error(error)
        }
      },
      sort() {
        state.sort = !state.sort
        logger.log(state.sort)
      }
    }
  }
}
</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}

table.table-bordered > thead > tr > th{
  border:3px solid #00ff00;
}

.text{
    color: #00ff00;
}

.btn-outline-green{
    border-color: #00ff00;
}
</style>
