var MyPlugin = {
    SendScore: function (score) {
    	gameManagement.sendScore(score);
    },

    GetConfig: function () {
    	return gameManagement.getConfig()
  	},
};
mergeInto(LibraryManager.library, MyPlugin);