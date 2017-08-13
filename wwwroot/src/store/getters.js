export function getNotifications (state) {
  return propertyDetailsId => state.notifications.filter(notification => {
    return notification.propertyDetailsId === propertyDetailsId
  }) || []
}
